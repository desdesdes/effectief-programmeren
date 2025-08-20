md source
cd source

md HardLinks
cd HardLinks
echo FirstHardLink > FirstHardLink.txt
echo SecondHardLink > SecondHardLink.txt
cd ..

md Junction
cd Junction
echo Junction > Junction.txt
cd ..

md SoftLinkDir
cd SoftLinkDir
echo SoftLinkDir > SoftLinkDir.txt
cd ..

md SoftLinkFile
cd SoftLinkFile
echo FirstSoftLinkFile > FirstSoftLinkFile.txt
echo SecondSoftLinkFile > SecondSoftLinkFile.txt
cd ..

cd ..
md destination
cd destination

mklink /H hardlink1.txt ..\source\HardLinks\FirstHardLink.txt
mklink /H hardlink2.txt ..\source\HardLinks\SecondHardLink.txt
mklink /J Junction ..\source\Junction
mklink /D SoftLinkDir ..\source\SoftLinkDir
mklink FirstSoftLinkFile.txt ..\source\SoftLinkFile\FirstSoftLinkFile.txt
mklink SecondSoftLinkFile.txt ..\source\SoftLinkFile\SecondSoftLinkFile.txt

cd..