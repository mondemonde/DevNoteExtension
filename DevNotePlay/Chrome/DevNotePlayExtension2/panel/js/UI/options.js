/*
 * Copyright 2017 SideeX committers
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 *
 */
/* KAT-BEGIN not supported
$(document).ready(function() {

    browser.storage.sync.get("tac")
        .then((res) => {
            $("#tac").prop("checked", res.tac);
        });

    $("#tac").click(function() {
        browser.storage.sync.set({
            tac: $("#tac").prop("checked")
        });
    });

});
KAT-END */