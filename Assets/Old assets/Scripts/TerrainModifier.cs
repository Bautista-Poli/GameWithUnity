using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // ← AÑADIR ESTO
public class TerrainModifier : MonoBehaviour
{
    public LayerMask groundLayer;
    public Inventory inv;
    float maxDist = 4;

    void Update()
    {
        // Debug visual del raycast (línea roja)
        Debug.DrawRay(transform.position, transform.forward * maxDist, Color.red);

        Mouse mouse = Mouse.current;
        if (mouse == null)
        {
            Debug.LogWarning("Mouse no detectado!");
            return;
        }

        bool leftClick = mouse.leftButton.wasPressedThisFrame;
        bool rightClick = mouse.rightButton.wasPressedThisFrame;

        if(leftClick || rightClick)
        {
            Debug.Log("========== CLICK DETECTADO ==========");
            Debug.Log("Left click: " + leftClick + ", Right click: " + rightClick);
            
            RaycastHit hitInfo;
            
            Debug.Log("Posición raycast: " + transform.position);
            Debug.Log("Dirección raycast: " + transform.forward);
            Debug.Log("Distancia máxima: " + maxDist);
            Debug.Log("Ground Layer value: " + groundLayer.value);
            
            if(Physics.Raycast(transform.position, transform.forward, out hitInfo, maxDist, groundLayer))
            {
                Debug.Log("✓ RAYCAST GOLPEÓ!");
                Debug.Log("Objeto: " + hitInfo.collider.name);
                Debug.Log("Layer: " + LayerMask.LayerToName(hitInfo.collider.gameObject.layer));
                Debug.Log("Distancia: " + hitInfo.distance);
                
                Vector3 pointInTargetBlock;

                if(rightClick)
                    pointInTargetBlock = hitInfo.point + transform.forward * .01f;
                else
                    pointInTargetBlock = hitInfo.point - transform.forward * .01f;

                int chunkPosX = Mathf.FloorToInt(pointInTargetBlock.x / 16f) * 16;
                int chunkPosZ = Mathf.FloorToInt(pointInTargetBlock.z / 16f) * 16;

                ChunkPos cp = new ChunkPos(chunkPosX, chunkPosZ);
                
                Debug.Log("Chunk position: " + cp.x + ", " + cp.z);

                if(!TerrainGenerator.chunks.ContainsKey(cp))
                {
                    Debug.LogError("✗ Chunk no encontrado en diccionario!");
                    return;
                }

                TerrainChunk tc = TerrainGenerator.chunks[cp];

                int bix = Mathf.FloorToInt(pointInTargetBlock.x) - chunkPosX + 1;
                int biy = Mathf.FloorToInt(pointInTargetBlock.y);
                int biz = Mathf.FloorToInt(pointInTargetBlock.z) - chunkPosZ + 1;
                
                Debug.Log("Block index: " + bix + ", " + biy + ", " + biz);
                Debug.Log("Block type: " + tc.blocks[bix, biy, biz]);

                if(rightClick) // Romper bloque
                {
                    Debug.Log("→ Intentando ROMPER bloque: " + tc.blocks[bix, biy, biz]);
                    
                    if(inv == null)
                    {
                        Debug.LogError("✗ Inventory es NULL!");
                        return;
                    }
                    
                    inv.AddToInventory(tc.blocks[bix, biy, biz]);
                    tc.blocks[bix, biy, biz] = BlockType.Air;
                    tc.BuildMesh();
                    
                    Debug.Log("✓ Bloque roto exitosamente!");
                }
                else if(leftClick) // Colocar bloque
                {
                    Debug.Log("→ Intentando COLOCAR bloque");
                    
                    if(inv == null)
                    {
                        Debug.LogError("✗ Inventory es NULL!");
                        return;
                    }
                    
                    if(inv.CanPlaceCur())
                    {
                        BlockType blockToPlace = inv.GetCurBlock();
                        Debug.Log("→ Colocando: " + blockToPlace);
                        
                        tc.blocks[bix, biy, biz] = blockToPlace;
                        tc.BuildMesh();
                        inv.ReduceCur();
                        
                        Debug.Log("✓ Bloque colocado exitosamente!");
                    }
                    else
                    {
                        Debug.LogWarning("✗ No tienes bloques para colocar!");
                    }
                }
            }
            else
            {
                Debug.LogWarning("✗ RAYCAST NO GOLPEÓ NADA!");
                Debug.LogWarning("Verifica que:");
                Debug.LogWarning("1. Los chunks tengan Layer 'Ground'");
                Debug.LogWarning("2. Ground Layer esté configurado en TerrainModifier");
                Debug.LogWarning("3. Los chunks tengan MeshCollider");
            }
        }
    }
}