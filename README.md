# TFS

# TFS History Tool

Use case:
- To gather all check-ins from the TFS branch and prepare release notes

UI Mode:
- open TFSChangeHistory.exe
- Provide the following inputs
    - TFS URL - For example: http://yourcompany/tfs/defaultcollection
    - Release Branch - For example: $/Company/Product/Release/ReleaseName
    - Ignore Check-ins from users - Optional - For Example - DOMAIN/USER1,DOMAIN/User2
    - From / To Date - Check-In date range used to filter the results


- Optionally default values for these three fields can be populated from app.config
    - TFSApiUrl - For example: http://yourcompany/tfs/defaultcollection
    - TFSReleaseBasePath - For example: $/Company/Product/Release
    - IgnoreChangesetFromOwner - For example - DOMAIN/USER1,DOMAIN/User2

Command line mode:
```sh
$ TFSHistory.exe [options]>
```
Run TFSHistory -? for details
