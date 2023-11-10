$ResourceGroupName="pw-gp100"
$Location="North Europe"
$AppServicePlanName="pw-plan100"
$WebAppName="pw-webapp100"

Connect-AzAccount
New-AzResourceGroup -Name $ResourceGroupName -Location $Location

New-AzAppServicePlan -ResourceGroupName $ResourceGroupName -Location $Location _tier "B1" -NumberofWorkers 1 -Name $AppServicePlanName

New-AzWebApp -ResourceGroupName $ResourceGroupName -Name $WebAppName -Location $Location -AppServicePlan $AppServicePlanName
