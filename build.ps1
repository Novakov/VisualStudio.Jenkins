function Invoke-Build 
{
    param($Solution, $Target, $Configuration, $LogFile)

    $logger = ''

    if($LogFile -ne $null)
    {
        $logger = "/l:FileLogger,Microsoft.Build.Engine;logfile=$($LogFile)"
    }

    Exec { msbuild $Solution /t:$Target /p:Configuration=$Configuration $logger /v:minimal }
}

Task Default  -depends Build

Task Build {
   Invoke-Build -Solution .\JenkinsBuilds.sln -Target Rebuild -Configuration Release -LogFile BuildLog.log
}
