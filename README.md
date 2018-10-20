# UModule Basic Structures and Extensions

## Contents
- Containers
- CurveAsset (+ CurveAssetExtensions)

## Setup
- Navigate to the desired install location inside a Unity project git repository

  `cd Assets/Modules`
- Inside the target folder, add this repository as a submodule

  `git submodule add https://github.com/rajin-s/UM_Basic.git`
- Commit the changes to finish registering the submodule

  `git add --all; git commit -m "Added UM_Basic submodule"`
- Initialize the submodule from anywhere in the parent repository

  `git submodule init`

## Updating
_requires setup as a git submodule according to the above instructions_
- Inside the parent Unity project repository, enter
  `git submodule update --remote`
  to get the latest version of this submodule

## License
Available under the GNU AGPL3 License (see LICENSE file)