#!/bin/bash
#chmod u+rwx configs/install.sh
cd configs
bower install
nuget install -OutputDirectory ../packages/
cd ..
rm -r warptrail/Assets/Packages/StrangeIoC
cp -r packages/strangeioc/StrangeIoC/scripts/strange/ warptrail/Assets/Packages/StrangeIoC
echo "Done"
