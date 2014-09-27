function HideSideNavigation()
{
    document.getElementById("sideNavContentDiv").style.display = "none";
    document.getElementById("mainContentDiv").setAttribute("class", "span12");
    document.getElementById("mainContentDiv").style.setAttribute("marginLeft", "0px");
}

function RestoreSideNavigation()
{
    document.getElementById("sideNavContentDiv").style.display = "block";
    document.getElementById("sideNavContentDiv").setAttribute("class", "span3");
    document.getElementById("mainContentDiv").setAttribute("class", "span9");
    document.getElementById("mainContentDiv").style.setAttribute("marginLeft", "2.56%");
}