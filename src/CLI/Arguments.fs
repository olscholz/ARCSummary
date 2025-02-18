﻿namespace ARCSummary


open Argu


module mainCLI =
    
    type SummaryArgs =
        | [<Mandatory>] [<AltCommandLine("-d")>][<AltCommandLine("-ap")>] ARC_Directory of arcPath : string 
        interface IArgParserTemplate with
            member s.Usage =
                match s with
                | ARC_Directory  _ -> "Location of the ARC in the Filesystem" 

    and SummaryMRArgs =
        | [<Mandatory>] [<AltCommandLine("-d")>] ARC_Directory of string
        | [<Mandatory>] [<AltCommandLine("-t")>] Token of string
        | [<Mandatory>] [<AltCommandLine("-i")>] PathOrId of string 
        | [<AltCommandLine("--message")>] CommitMessage of string 
        | [<AltCommandLine("--title")>] MRTitle of string 
        | SourceBranch of string 
        | TargetBranch of string 
        | [<AltCommandLine("--name")>] UserName of string
        | [<AltCommandLine("--email")>] UserEmail of string
        | [<AltCommandLine("--api")>] APIAdress of string
        interface IArgParserTemplate with
            member s.Usage =
                match s with
                | ARC_Directory  _ -> "Location of the ARC in the Filesystem" 
                | Token _ -> "Personal access token for gitlab"
                | PathOrId _ -> "ID or URL-encdoded path of the project after .org/, e.g. username/myprojectname"
                | CommitMessage _ -> "Message to be used for the commit."
                | MRTitle _ -> "Title of the Merge Request"
                | SourceBranch _ -> "Name of the branch to which the commit should be pushed, and which will be the source branch of the MR. Default is `arc-summary`"
                | TargetBranch _ -> "Name of the reference branch which is the target for the MR. Default is `main`"
                | UserName _ -> "Username to be used for the commit"
                | UserEmail _ -> "Email to be used for the commit"
                | APIAdress _ -> "Testing Server URL"

    and CLIArgs =
        | [<CliPrefix(CliPrefix.None)>] Summary of ParseResults<SummaryArgs>
        | [<CliPrefix(CliPrefix.None)>] SummaryMR of ParseResults<SummaryMRArgs>       

        interface IArgParserTemplate with
            member s.Usage =
                match s with 
                | Summary _ -> "Updates your README.md to current version"
                | SummaryMR _ -> "Pushes Updated Summary to side branch and opens a MergeRequest onto main branch."    
