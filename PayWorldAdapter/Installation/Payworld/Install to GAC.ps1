Set-location "c:\Temp\KKN\Payworld\PayWorldAdapter_v1\"            
[System.Reflection.Assembly]::Load("System.EnterpriseServices, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")            
$publish = New-Object System.EnterpriseServices.Internal.Publish            
$publish.GacInstall("c:\Temp\KKN\Payworld\PayWorldAdapter_v1\PayWorldAdapter.dll") 
          
