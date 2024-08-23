# How to contribute to the project
Any help is welcome.
Please follow the guidelines that you can find in this document.

## Project-Structure
```
├───Assets
│   ├───Core
│   │   ├───Assets
│   │   │   └───Sprites
│   │   ├───Prefabs
│   │   ├───Resources (revision is needed) (+)
│   │   ├───Scripts
│   │   └───Tilemaps
│   │       ├───Tilemap1
│   │       └───Tilemap2
│   ├───Resources (revision is needed) (-)
│   ├───Scenes
│   │   ├───Scene1
│   │   │   ├───Assets
│   │   │   │   └───Sprites
│   │   │   ├───Prefabs
│   │   │   ├───Resources (revision is needed) (+)
│   │   │   ├───Scripts
│   │   │   ├───Settings (revision is needed) (+)
│   │   │   └───Tilemaps
│   │   │       ├───Tilemap3
│   │   │       └───Tilemap4
│   │   └───Scene2
│   └───Settings
│       └───Scenes
├───Packages
└───ProjectSettings
```

### Assets
This folder holds all the user defined resources like sprites and scripts.
- `Core` subfolder holds all the reusable assets
- `Scenes` subfolder holds individual scenes with their folders.
- `Resources` subfolder's purpose should be revised, moving under core and scene can help modularization
- `Settings` global settings of the application

Both `Core` and `Scenes/<Individual Scene>` folders can have the following structure to organise assets.
- `Assets` - for static resources, like sprites
- `Prefabs`
- `Resources` - for external resources (e.g.: Android store)
- `Scripts`
- `Settings` - settings of the scene (scene only)
- `Tilemaps`

### Packages
TBD

### Project Settings
TBD
