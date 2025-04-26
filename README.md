# ✨🕺 CuteDancer v4.0 🕺✨

### [Add to VRChat Creator Companion](https://theunifox.github.io/CuteDancer/)
▶️ https://theunifox.github.io/CuteDancer/ ◀️

---

CuteDancer is a package dedicated to use on [VRChat](https://hello.vrchat.com/) avatars. It contains dance animations that can be played in sync together with other players who have the package installed on their avatars. You can use it for syncing animations for video recording, making funny public avatars or just for fun!

👉 [You can try it on public avatars here.](https://vrchat.com/home/world/wrld_deb6ff93-c907-4d16-92d0-911758135c70)

![promo anim](docs/images/cutedancer.gif)

### How does it work?

It uses [Contacts Components](https://creators.vrchat.com/avatars/avatar-dynamics/contacts/). When one avatar starts dancing, the sender component is activated and receivers on other avatars play that animation as well.

### What's new in v4.1?

- Now using VRCFury to add the dances to your avatar!
- Support for more dances than the VRC Contact Receiver's Collison Tags limit
- ~~Support for animations with an Intro and Loop section~~ TODO
- ~~Putting CuteDancer in a submenu~~ TODO

### What's new in v4.0?

- VRChat Creator Companion support
- Completely rewritten setup script with dance selector
- Support for adding custom animations
- Other small improvements and fixes

## Updating from old Krysiek v4.0 CuteDancer

If you had CuteDancer before Using this updated version (v4.0 or earlier), you will need to uninstall and remove the old Krysiek v4.0 CuteDancer.

Uninstall CuteDancer package by clicking `Manage Project` next to the desired avatar project, and then hit the red minus sign next to the CuteDancer package.

Remove the listing by going to Settings -> Packages -> and scrolling down under Installed Repositories, then hit the minus sign next to the CuteDancer Listing entry.

And finally follow the below Installation section.

## Installation

You have to use [VRChat Creator Companion](https://vcc.docs.vrchat.com/) for your Unity VRChat project.

Add the listing from https://theunifox.github.io/CuteDancer/

Install CuteDancer package by clicking `Manage Project` next to the desired avatar project and then choose the newest version available next to the CuteDancer package.

Install VRCFury by doing the same in the VCC.

## Basic usage

Open the setup by selecting from top bar `Tools` -> `CuteDancer` -> `CuteDancer Setup`

There are two tabs: `Builder` and `Installer`.

### Builder

This tab allows you to prepare CuteDancer prefab with selected dances, include or exclude audio and specify additional parameters. Your avatar on the scene won't be touched yet.

1. Select dances which you want to be included.
2. Click on the audio icon to include or exclude audio for the specific dance. This will save audio sources slots later on your avatar.
3. Click on the `Generate files` button.

<details>
<summary>Advanced settings</summary>

- `Name` - you can create multiple builds by changing its name (e.g. one for PC and one for Quest version).
- `Parameter name` - name of the parameter used for the dances. Change it if you have problem with non working animations, stuck (e.g. GoGo Loco).
- `Parameter start value` - indicates the start
- `Use VRCFury` - Whether to use a VRCFury prefab, or add directly to avatar files like pre v4.1. Will only show up if VRCFury is installed

</details>

### Installer

This tab allows you to apply generated files above on your avatar.

1. In `CuteDancer build` you can change which build will be applied to the avatar.
2. In `Avatar` field select your avatar from the scene. Fields with specific avatar components should be filled automatically.
3. Click `Apply to avatar` button

## Custom animation support

Select from top bar `Tools` -> `CuteDancer` -> `Create Dance Template`

The template dance will be created in `Assets/CuteDancer/Dances/MySuperDance` directory. Now you can edit it by renaming and replacing included files. It is recommended to rename the files in Unity, otherwise you have to keep renaming both, asset file and corresponding `.meta` file. Replacing the files is recommended outside of Unity.

<details>
<summary>Description of the info.json properties</summary>

- `name` - Used for internal parameters names, should be unique without spaces.
- `displayName` - Used for displaying in Builder and avatar's Action Menu.
- `collection` - Name of a category for the builder.
- `author` - Name of the author of the animation.
- `order` - Display order in the builder.

</details>

## Contributors

_VRCFury suport, more dances support, Intro animation support: [TheUnifox](https://github.com/TheUnifox)
Default animations, package: [Krysiek](https://github.com/Krysiek)  
Sender/Receiver config, support and tests: [Luc4r](https://github.com/Luc4r)  
Animators optimization, tests: [Jack'lul](https://github.com/jacklul)_