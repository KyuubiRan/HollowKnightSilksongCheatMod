# Hollow Knight: Silksong Cheat Mod

[中文说明](README_zh.md)

Use [BepInEx](https://github.com/BepInEx/BepInEx) to load this mod.

### For OSX Apple Silicon

Add this to your Steam launch options:  
`/usr/bin/arch -x86_64 /bin/bash "/full/path/to/run_bepinex.sh" %command%`
And modify `/full/path/to/run_bepinex.sh` to your script full path.

## Build

Copy the `Directory.Build.props.template` to `Directory.Build.props` and set the `GamePath` to your game path.

If you modified the language.json file, please **REBUILD** the project to include the changes.

## Features

### Player - Health

- [x] God mode
- [x] Lock max HP
- [x] Heal
- [x] Lock blue health state
- [x] Enter blue health state

### Player - Silk

- [x] Lock max silk
- [x] Refill to max

### Player - Noclip

- [x] Toggle noclip
- [x] Custom noclip speed

### Player - Damage

- [x] Multi damage
- [x] One hit kill

### Player - Action

- [x] Infinity air jump
- [x] No attack CD
- [x] No dash cooldown
- [x] Can infinity dash on air

### Player - Enhanced Attack

- [x] Attack forward
- [x] Attack upward
- [x] Attack downward
- [x] Attack backward
- [x] Attack right side
- [x] Attack left side

### Player - Kill Aura

- [x] Range filter
- [x] Custom damage
- [x] Custom trigger interval

### Player - Crest

- [x] Force reaper mode

### Inventory

- [x] Equip anywhere
- [x] Auto replenish count item
- [x] Infinity item use

### Enemy - Info

- [x] Show enemy HP

### Currency

- [x] Auto collect rosaries
- [x] Add/Remove rosaries
- [x] Auto collect shell shards
- [x] Add/Remove shell shards

### Game

- [x] Change game speed
- [x] FPS limiter(Should tun off VSync)

### World

- [x] Full bright in darkness region

### Menu - Map

- [x] Always show player's position on the map
- [x] Unlock map
- [ ] Update traveled areas on the map

### Teleport

- [x] Custom teleport
- [x] Death point log & teleport