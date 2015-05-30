#!/bin/bash

DIR=$(dirname "$0")
VERSION=$(git describe --abbrev=0 --tags)
REVISION=$(git log "$VERSION..HEAD" --oneline | wc -l)

re="([0-9]+\.[0-9]+\.[0-9]+)"
if [[ $VERSION =~ $re ]]; then
	VERSION_STR="${BASH_REMATCH[1]}.$REVISION"
	echo "Version is now: $VERSION_STR"
	
	lead='^\/\/ TRAVIS\-CI: START REMOVE$'
	tail='^\/\/ TRAVIS\-CI: END REMOVE$'
	sed -e "/$lead/,/$tail/{ /$lead/{p; r insert_file
        }; /$tail/p; d }"  $DIR/../IPTables.Net/Properties/AssemblyInfo.cs
	echo "[assembly: AssemblyVersion(\"$VERSION_STR\")]" >> $DIR/../IPTables.Net/Properties/AssemblyInfo.cs
	echo "[assembly: AssemblyFileVersion(\"$VERSION_STR\")]" >> $DIR/../IPTables.Net/Properties/AssemblyInfo.cs
fi