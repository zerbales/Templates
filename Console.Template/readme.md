----------------------------------
- RESOURCE GROUP -
- create - 
az group create --name "zerbax-group" --location "EastUS"
- delete -
az group delete --name "zerbax-group"
----------------------------------
- KEY VAULT -
- create -
az keyvault create --name "zerbax-keyvault" --resource-group "zerbax-group" --location "EastUs"
- set secret -
az keyvault secret set --vault-name "zerbax-keyvault" --name "pwd" --value "hVFkk965BuUv"
- set multiline secret -
az keyvault secret set --vault-name "zerbax-keyvault" --name "Multiline" --file "multiline.json"
- show -\
az keyvault secret show --name "Multiline" --vault-name "zerbax-keyvault" --query "value"
- ---------------------------------
- CONTAINER -
- create -
az container create --resource-group "zerbax-group" --name "zerbax-cnt" --image mcr.microsoft.com/azuredocs/aci-helloworld --dns-name-label aci-zerbax-demo --ports 80
- show -
az container show --resource-group zerbax-group --name zerbax-cnt --query "{FQDN:ipAddress.fqdn,ProvisioningState:provisioningState}" --out table
- logs -
az container logs --resource-group zerbax-group --name zerbax-cnt
- stream attach -
az container attach --resource-group zerbax-group --name zerbax-cnt
- 




-----------------------------------
VM
zerbax-vm
ADMIN VM
zerbax
PasswordAz204_
------------------------------------
SQL
Create Table Products(
	ProductId int,
	ProductName varchar(1000),
	Quantity int
)

Insert Into Products(ProductID, ProductName, Quantity) Values (1, 'Mobile', 100)
Insert Into Products(ProductID, ProductName, Quantity) Values (2, 'Laptop', 200)

-----------------------------------
DOCKER ON VM
Install docker on VM 
Create dockerfile in project
Build image
sudo docker build -t (name) .
Run container
sudo docker run --name (name-container) -p 80:80 -d (name-image)
List container running
sudo docker ps
Stop container
sudo docker stop (id-container)
List images
sudo docker images
-----------------------------------
MYSQL
CREATE DATABASE zerbaxdb;

USE zerbaxdb;
Create Table Products(
	ProductId int,
	ProductName varchar(1000),
	Quantity int
);

INSERT Into Products(ProductID, ProductName, Quantity) Values (1, 'Mobile', 100);
Insert Into Products(ProductID, ProductName, Quantity) Values (2, 'Laptop', 200);

sudo docker run --name=mysql-instance -p 3306:3306 --restart on-failure -d -e MYSQL_ROOTPASSWORD=PasswordAz204_ mysql

----------------------------------
AZ POWERSHELL
install version 7
install az powershell module


