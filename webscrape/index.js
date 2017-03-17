/////////////////////////////////////////////////
// Tekstovi.net Web Scrape 
// Author : Addison B 
// Date   : 2017-03-17 

"use strict";

// using osmosis web scraping framework 
var osmosis = require('osmosis');  

// our base url for the site we are going to scrape 
const baseUrl = "http://tekstovi.net";

const WebScrape = require("./WebScrape.js");

var webScrape = new WebScrape(baseUrl, osmosis);


webScrape.searchBySongName("zivot");
