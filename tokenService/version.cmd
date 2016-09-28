@echo off
echo Writing %SCM_COMMIT_ID% to "%DEPLOYMENT_TARGET%\version.txt"
echo %SCM_COMMIT_ID% > "%DEPLOYMENT_TARGET%\version.txt"