//////////////////////////////////////////////
// WebScrape
// Author      : Addison Benzshawel
// Description : JS wrapper around Osmosis scraping libary 
//              (https://github.com/rchipka/node-osmosis)

"use strict";

var WebScrape = function(baseUrl, osmosisLib) {
    ///////////////////////////////////////////
    // Parameter Validation

    if (typeof (baseUrl) !== "string") {
        console.warn("Invalid parameters for WebScrape! baseUrl must be of type string.")
        return;
    }

    if ((typeof (baseUrl) === "string" && baseUrl.length === 0) ||!validateUrl(baseUrl)) {
        console.warn("WebScrape baseUrl parameter must be a valid url.");
        return;
    }

        
    if (typeof(osmosisLib) === "undefined") {
        console.warn("WebScrape missing osmosis library!");
        return;
    }


    this.baseUrl = baseUrl

    WebScrape.osmosis = osmosisLib;

    // WebScrape.osmosis  
    //     .get(this.baseUrl)
    //     .set({'Title': 'title'})   // or alternate: `.find('title').set('Title')`
    //     .data(console.log)  // will output {'Title': 'Google'}

    /////////////////////////////////////////////
    // Private Functions 
    function validateUrl(value) {
        return /^(?:(?:(?:https?|ftp):)?\/\/)(?:\S+(?::\S*)?@)?(?:(?!(?:10|127)(?:\.\d{1,3}){3})(?!(?:169\.254|192\.168)(?:\.\d{1,3}){2})(?!172\.(?:1[6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)(?:\.(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)*(?:\.(?:[a-z\u00a1-\uffff]{2,})))(?::\d{2,5})?(?:[/?#]\S*)?$/i.test(value);
    }

}

WebScrape.prototype.searchBySongName = function (searchTerm) {
    return this.search(searchTerm, 1);
}

/////////////////////////////////////////////////
// Tesktovi Search
// The site's search structure works like "/SEARCH_TERM,IS_ARTIST,IS_SONG"" 
// where 
//     SEARCH_TERM = string to query 
//     SEARCH_TYPE = 0 for artist / performer, 1 for name of songs, 2 for part of text  
//     UNDETERMINED_FLAG = 0 for all searches (as far as I can tell)
// Example URL to serach songs with the name "budalo"
//     http://tekstovi.net/budalo,1,0.html
//
WebScrape.prototype.search =  function (searchTerm, searchType) {
    var endPoint = this.baseUrl + "/" + searchTerm + "," + searchType + ",0.html";
    //console.log(endPoint);
     WebScrape.osmosis.get(endPoint)
        .set({
            "pageText" : "body"
        })
        .data(function (result) {
            // remove line feeds and tabs 
            var cleaned = result.pageText.replace(/(\r\n|\n|\r)/gm,"").replace(/\t/g, "");
            // break up the data for results and links 
            var startOfSearchResults = "Broj pronađenih rezultata";
            var resultsStartIndex = cleaned.indexOf(startOfSearchResults);
            // split string here 
            var searchBreakPoint = "Tekstovi.net-a: ";
            var searchBreakPointIndex = cleaned.indexOf(searchBreakPoint);
            
            var searchText = "";
            var searchLinks = "";
            if (resultsStartIndex > -1 && searchBreakPointIndex > -1) {
                 searchText = cleaned.substring(resultsStartIndex, searchBreakPointIndex);
                 searchLinks = cleaned.substr(searchBreakPointIndex);
            }

            console.log("Search Text");
            console.log(searchText);

            console.log("Search Links");
            console.log(searchLinks);
        });
}

module.exports = WebScrape;