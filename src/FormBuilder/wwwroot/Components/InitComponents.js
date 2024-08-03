/**
 * Loads bootstrap styles and Radzen.Blazor.js files
 */
export function loadStaticFiles() {
    const link = document.createElement("link");
    link.rel = "stylesheet";
    link.href = "_content/FormBuilder/css/bootstrap/bootstrap.min.css";
    document.head.appendChild(link);
    
    const script = document.createElement("script");
    script.src = "_content/Radzen.Blazor/Radzen.Blazor.js";
    document.body.appendChild(script);

    console.log("Bootstrap and Radzen static files loaded");
}
