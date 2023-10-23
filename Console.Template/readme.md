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
