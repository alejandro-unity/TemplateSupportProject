

// Hold option + Cmd to drag this script to the Finder Tool bar

cdf() {

	target=`osascript -e 'tell application "Finder" to if (count of Finder windows) > 0 then get POSIX path of (target of front Finder window as text)'`
	if [ "$target" != "" ]; then
		cd "$target"; pwd
	else
		echo 'No Finder window found' >&2
	fi
}

echo "Get path the from Finder "

cdf

echo "Copying Unity Support files "

pwd

cp -rf  /Users/unity/Documents/Unity-GitHub/TemplateSupportProject/Assets/Editor/  ./Assets/Editor/
cp -rf  /Users/unity/Documents/Unity-GitHub/TemplateSupportProject/Assets/Scripts/ ./Assets/Scripts/

exit