#!/bin/bash

if [ "$(uname)" == 'Darwin' ]  && [ "$(uname -m)" == 'x86_64' ]; then
	CHECKED_OS='Mac(x86_64)'
elif [ "$(uname)" == 'Darwin' ]  && [ "$(uname -m)" == 'arm64' ]; then
	CHECKED_OS='Mac(arm64)'
elif [ "$(uname)" == 'Linux' ]; then
	CHECKED_OS='Linux'
elif [ "$(expr substr $(uname) 1 5)" == 'MINGW' ]; then
	CHECKED_OS='MinGW'
elif [ "$(expr substr $(uname) 1 6)" == 'CYGWIN' ]; then
	CHECKED_OS='Cygwin'
else
	echo "Your platform ($(uname -a)) is not supported."
	CHECKED_OS="Unknown"
fi
echo "Checked OS:${CHECKED_OS}"
