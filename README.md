# Lila Machinima Mod

This Who is Lila mod aims to introduce functionalities that will assist you in creating media (videos, memes) related to Who is Lila. Uses MelonLoader.

## Installation

1. You will need to have the MelonLoader installed: [MelonLoader's site](https://melonwiki.xyz/).
1. Download the latest .dll from the Releases tab.
1. Navigate to game's installation directory and place the .dll in the Mods directory (`Who's Lila\Mods\`).
1. In the same directory, add `video.txt` and `dialog.txt` files so that the mod functions correctly. 

## Setup

### video.txt
In `video.txt` provide the path to the clip that you would like to display in the game. For example: `C:/Users/User/Downloads/video.mp4`

### dialog.txt
In this file provide the dialog that you want to display in the game. It has to be in the Who is Lila format:
- `[HeaderName]` - header of the dialog. Currently has to be set to `Header1`.
- `(CharacterName)` - name of the character that is currently speaking.
- `(CharacterName, emotion)` - as above, but with selected emotion for the character's portrait.
- `^SFXName` - name of SFX from the game that should be played.
- `{name, duration}` - launches the face minigame with the provided name and duration in seconds. 
- `{name, difficulty, confidence, duration}` - launches the harder version of the face minigame using the provided difficulty factor and "confidence pose" (not sure what this does). 
- `$CustomMethod` Launches a method from the game (unsure how to use now).
- `>HeaderName` Proceeds to the dialogue under the provided header name. 

The actual text can be wrapped in `<i>` for cursive. 

Example:
```
[Header1]
(Mike, Neutral) Hello.
^SFX_MildScare
(null) Hello. <i>Hello</i>.
```
## Usage
If you've provided the files above, you can launch the video using `F6` key. The dialog is launched using `F5` key.
