# dotnet-core-web-api-caching-examples
This repo has .NET Core Web API examples with and without caching to go with a Medium.com article I wrote. The article is at https://medium.com/@dale.bingham_30375/adding-response-caching-to-your-net-core-web-apis-quickly-3b09611ae4f5.

## How to use this repo
You can clone this repo and then run `dotnet build` in both caching and non-caching folders here.  

There is a webui folder with HTML/CSS/JS in there that points to localhost:8000 and localhost:8002 for each of the APIs. You can run that HTML in a browser with whatever means necessary. I just run `python3 -m http.server 9000` from my Mac to run it locally. 

If you use the Dev Tools in Chrome, Firefox, Safari, etc. you can note the network time on the calls. The caching initial call should be the same as the non-caching. After that the same call should be faster.
