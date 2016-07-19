$wikiRepository = 'https://github.com/mike-dempster/Pelorus.wiki.git'
$gitPath = 'Pelorus.wiki'
$projectPath = '.\Pelorus.Documentation'
$accessToken = $env:github_token

try {
	Push-Location
	cd $projectPath
	$repoExists = Test-Path ".\$gitPath"

	if ($repoExists) {
		# Delete the reporitory if it has already been cloned
		Remove-Item ".\$gitPath" -Force -Recurse
	}

	# Configure the Git environment
	git config --global push.default simple
	git config --global core.autocrlf true
	git config --global user.name "Build Process"
	git config --global user.email "build.process@appveyor.com"
	git config --global credential.helper store

	# Save the credentials to use for GitHub requests
	Add-Content "$env:USERPROFILE\.git-credentials" "https://$($accessToken):x-oauth-basic@github.com`n"

	# Clone the repository, remove the old files, add the new files, commit, and push the new documentation to the server
	git clone $wikiRepository
	cd $gitPath
	git rm *
	Copy-Item '..\Help\*' '.\'
	git add *
	git commit -m "Updating documentation"
	git push
} catch {
	throw
} finally {
	$repoExists = Test-Path "..\$gitPath"

	if ($repoExists) {
		cd ..\
		Remove-Item ".\$gitPath" -Force -Recurse
	}

	Pop-Location
}
