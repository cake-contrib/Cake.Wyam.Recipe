---
Order: 10
---

The various tasks within Cake.Wyam.Recipe are driven, in part, by whether the correct environment variables exist on the system where the build is executing.

The following are a list of the default environment variable names that are looked for during the build.

**NOTE:** If required, the name of the environment variables can be modified to fit into your existing system/architecture.

# GitHub

## GITHUB_USERNAME

User name of the GitHub account used to create and publish releases.

## GITHUB_PASSWORD

Password of the GitHub account used to create and publish releases.

# AppVeyor

## APPVEYOR_API_TOKEN

API token for accessing AppVeyor. Used to [clean AppVeyor build cache](../usage/cleaning-cache).

# Wyam

## WYAM_ACCESS_TOKEN

Access token to use to publish the Wyam documentation. Used to [publish documentation](../usage/publishing-documentation).

## WYAM_DEPLOY_REMOTE

URI of the remote repository where the Wyam documentation is published to. Used to [publish documentation](../usage/publishing-documentation).

## WYAM_DEPLOY_BRANCH

Branch into which the Wyam documentation should be published. Used to [publish documentation](../usage/publishing-documentation).