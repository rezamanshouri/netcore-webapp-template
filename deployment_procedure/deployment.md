# Github Flow Deployment Procedure
This documents aims at providing a detailed procedure to deploy an application using _Github flow_. Here are a few assumptions in this document:

1. There is only one branch, `main`, and tags are used to mark points in history from which a deployment has happened.
1. The project will be deployed on regular cycles, and deployment is a manual process.
1. There are three environments available:
    - Development
        - Deployment to this is environment is automated. Every time new changes are pushed to `main` branch it is auto-deployed to this environment.
    - Staging
        - This environment is where the new payload is validated before it is deployed to Production.
        - Deployment to this environment is manual.
    - Production
        - This is the main Production environment.
        - Deployment to this environment is manual.
1. If a changelog is desired along with each deployment, update the following procedure to update the changelog file after the release/hotfix branch was created.


### Tags and Release Versioning

In _GitHub flow_ workflow, tags are used to mark points in the source control history at which deployment has happened. Tag names follow `<project_name>.v<major_version>.<minor_version>` format where `major_version` could simply be integers starting from 1 and incremented with every deployment, or _sprint_ number. The minor version is _0_ for the main release, _1_ for the first hotfix, _2_ for the second hotfix and so on so forth. For example:

- `my-app.v12.0` refers to the 12-th release.
- `my-app.v12.1` refers to the first hotfix for 12-th release.
- `my-app.v12.2` refers to the second hotfix for 12-th release.


## Release Procedure

1. Determine the `tag_name` following [these instructions](#tags-and-release-versioning).
1. Determine the `commit_hash` from which the deployment is desired and create the release branch off of that:
    - Make sure you are on the `main` branch and your local branch is up to date with remote.
    - Create release branch using `tag_name` from step 1, and push to remote:

    ```
        git checkout -b release.<tag_name> <commit_hash>
        git push
    ```
    - Create a PR for release branch.

1. Deploy the release branch created in previous step to Staging environment.

    - If there are any E2E tests, they could be kicked off as part of deploying to Staging.

1. Monitor logs (and E2E test results) in Staging. Once payload in Staging is validated, we are ready for deployment.
    - In case issues are detected in Staging:
        - Fix the issue on the release branch and push to remote.
        - Deploy the release branch again to Staging and validate.
1. Create a tag _on release branch_ and push to remote (where `tag_name` is same as step 1).
    ```
        git tag <tag_name>
        git push main <tag_name>
    ```
1. Confirm that the tag has been created on the remote.
1. Merge release branch back to `main` (similar to a regular PR).
1. Determine the deployment date (for example, the first upcoming Monday), and communicate with stakeholders the deployment day and time with a summary of changes. Schedule a meeting with all the developers to perform deployment at that day and time.
1. On the deployment date and before kicking off the deployment, let stakeholders know that the deployment is about to happen.
1. Deploy the `tag_name` to Production.
1. Monitor logs for outstanding issues right after deployment.


## Hotfix Procedure
When an issue detected in Production, hotfixing the issue should be prioritized. Here are the steps to follow:

1. Communicate with stakeholders that the issue is acknowledged and being worked on.
1. Find the latest tag deployed to Production. Let's call this tag `latest_tag_name`. Also, determine the `tag_name` for this hotfix following [these instructions](#tags-and-release-versioning).
1. Create a hotfix branch off of the `latest_tag_name`:
    ```
        git checkout -b hotfix.<tag_name> <latest_tag_name>
    ```
1. Fix and validate the issue locally. Push your changes to remote and create a PR and ask for reviews.
1. Deploy the hotfix branch to Staging, and validate the fix (run E2E tests if applicable).
1. Once the fix is validated in Staging, we are ready to deploy to Production. Create a tag _on hotfix branch_ and push to remote (where `tag_name` is same as step 2).
    ```
        git tag <tag_name>
        git push main <tag_name>
    ```
1. Merge hotfix branch back to `main` (similar to a regular PR).
1. Deploy the hotfix to Production.
1. Let stakeholders know that the fix is deployed (with possible root-cause analysis).
