npm run build:develop
msiexec.exe /i https://awscli.amazonaws.com/AWSCLIV2.msi
read -p "Press conitnue to configure AWS..."
aws configure
echo $(pwd)/build
aws s3 cp $(pwd)/build/ s3://zipcodemastekclient/ --recursive
read -p "Press any key..."