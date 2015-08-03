#!/bin/bash
#chmod u+rwx configs/install.sh
cd configs
bower install
nuget install -OutputDirectory ../packages/
cd ..
rm -r trailapp/Assets/Packages/StrangeIoC
cp -r packages/strangeioc/StrangeIoC/scripts/strange/ trailapp/Assets/Packages/StrangeIoC
echo "Done"
