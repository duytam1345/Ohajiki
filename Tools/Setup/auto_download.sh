#!/bin/bash

set -eu

SCRIPT_DIR=$(cd $(dirname $0); pwd)
source ${SCRIPT_DIR}/firebase.env
WORKSPACE=$(cd $(dirname ${SCRIPT_DIR}/../../../); pwd)
mkdir ${WORKSPACE}/GooglePackages || true

curl https://dl.google.com/games/registry/unity/com.google.firebase.analytics/com.google.firebase.analytics-${FIREBASE_VER}.tgz -o ${WORKSPACE}/GooglePackages/com.google.firebase.analytics-${FIREBASE_VER}.tgz

curl https://dl.google.com/games/registry/unity/com.google.firebase.remote-config/com.google.firebase.remote-config-${FIREBASE_VER}.tgz -o ${WORKSPACE}/GooglePackages/com.google.firebase.remote-config-${FIREBASE_VER}.tgz

curl https://dl.google.com/games/registry/unity/com.google.firebase.app/com.google.firebase.app-${FIREBASE_VER}.tgz -o ${WORKSPACE}/GooglePackages/com.google.firebase.app-${FIREBASE_VER}.tgz

EDM4U_VER=`tar xvzf ${WORKSPACE}/GooglePackages/com.google.firebase.app-${FIREBASE_VER}.tgz -O package/package.json | jq -r '.dependencies | ."com.google.external-dependency-manager"'`

curl https://dl.google.com/games/registry/unity/com.google.external-dependency-manager/com.google.external-dependency-manager-${EDM4U_VER}.tgz -o ${WORKSPACE}/GooglePackages/com.google.external-dependency-manager-${EDM4U_VER}.tgz 

echo "Done."
