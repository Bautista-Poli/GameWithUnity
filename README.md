# 🧱 **GameWithUnity – Proyecto en Unity (3D + Voxel Terrain + Inventario)**

### Un prototipo de juego 3D en Unity que incluye generación procedural de mundo tipo “voxel”, movimiento de personaje en tercera persona, cámara dinámica y un sistema básico de inventario.

---

## 🚀 **Características principales**

### 🗺️ **Mundo Procedural Estilo Voxel**

* Generado usando scripts como `TerrainGenerator`, `TerrainChunk`, `TerrainModifier`, `TilePos`, etc.
* Sistema de *chunks* que permiten cargar terrenos grandes.
* Representación tipo “cubos” al estilo Minecraft.

### 🧍 **Jugador 3D con Movimiento**

* Personaje totalmente animado (Idle, correr, etc.).
* Control mediante `PlayerMovement` + `playerController`.
* Detección de suelo (`GroundCheck`).
* Cámara colocada en **tercera persona** y siguiendo al jugador.

### 🎥 **Cámara**

Scripts relevantes:

* `MouseLook` (para control con el mouse).
* `CamFly` (modo libre / debug).

La cámara está integrada como hijo del `Player` para un seguimiento suave.

### 🎒 **Sistema de Inventario**

* UI en Unity (`Inventory UI`).
* Slots (`slot1`, `slot2`, etc.) con íconos individuales.
* Scripts:

  * `Inventory`
  * `InventoryPlayer`
  * `Block` (probable sistema de bloques/ítems)

### 🌊 **Agua & Terreno**

* `WaterChunk.cs` para la generación/animación del agua.

---

## 📁 **Estructura del Proyecto**

```
Assets/
│
├── Old assets/
│   ├── Scripts/
│   │   ├── Block.cs
│   │   ├── CamFly.cs
│   │   ├── FastNoise.cs
│   │   ├── Inventory.cs
│   │   ├── InventoryPlayer.cs
│   │   ├── MouseLook.cs
│   │   ├── PlayerMovement.cs
│   │   ├── TerrainChunk.cs
│   │   ├── TerrainGenerator.cs
│   │   ├── TilePos.cs
│   │   └── WaterChunk.cs
│
├── Player/
│   ├── Idle (modelo / animación)
│   ├── Main Camera
│   └── GroundCheck
│
├── UI/
│   └── Inventory UI (slots, icons)
│
└── World/
```

---

## 🛠️ **Tecnologías Utilizadas**

* **Unity 2021+**
* **C#**
* Sistema de generación procedural
* UI Unity (Canvas, Image, etc.)
* Shaders/Materiales para bloques y agua

---

## 🎮 **Controles**

*(modificá si querés)*

* **WASD** – movimiento
* **Mouse** – cámara
* **Espacio** – saltar
* **E** – abrir/cerrar inventario
* **1234** - para cambiar entre items

---

## 🏗️ **Roadmap (próximos pasos)**

* Agregar sistema de construcción y destrucción de bloques
* Implementar crafting
* Mejorar animaciones del personaje
* Optimizar chunks para mundos más grandes

---

## 📦 **Cómo Ejecutar el Proyecto**

1. Clonar el repositorio:

   ```bash
   git clone https://github.com/Bautista-Poli/GameWithUnity.git
   ```

2. Abrir Unity Hub → *Open project* → seleccionar la carpeta del repo.

3. Abrir la escena

4. Presionar Play para iniciar.


Si querés lo puedo **personalizar más**, agregar imágenes, GIF del juego, o incluso generar un banner para el README. ¿Querés algo más visual?
