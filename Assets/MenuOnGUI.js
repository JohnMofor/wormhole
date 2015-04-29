var MenuSkin : GUISkin;
 
function OnGUI() {
GUI.skin = MenuSkin;
            GUI.Button(Rect(Screen.width/2 - 75,Screen.height/2,150,75),"");
}