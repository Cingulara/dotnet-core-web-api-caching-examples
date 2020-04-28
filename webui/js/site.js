// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

async function getNonCachedContent() {
	let response = await fetch("http://localhost:8000/");
	if (response.ok) {
			var data = await response.json()
			alert('Finished Non-Cached Content Call');
	}
	else {
        alert('Please start the API endpoints');
	}
}

async function getCachedContent() {
	let response = await fetch("http://localhost:8002/");
	if (response.ok) {
			var data = await response.json()
			alert('Finished Cached Content Call');
	}
	else {
        alert('Please start the API endpoints');
	}
}
