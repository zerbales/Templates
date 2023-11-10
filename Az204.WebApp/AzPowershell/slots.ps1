$ResourceGroupName="pw-gp100"
$WebAppName="pw-webapp100"
$AppServicePlanName="pw-plan100"

$Propierties=@{
    repoUrl="https://github.com/zerbales/Templates";
    branch="master";
    isManualIntegration="true";
}

Set-AzResource -ResourceGroupName $ResourceGroupName -Properties $Propierties -ResourceType Microsoft.Web/sites/sourcecontrols -ResourceName $WebAppName/web -ApiVersion 2023-01-01 -Force