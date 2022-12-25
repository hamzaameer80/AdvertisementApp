/// <reference name="MicrosoftAjax.js"/>
 
var Label1, TextBox1;

Sys.Application.add_init(AppInit);

function AppInit(sender) {
  Label1 = $get('Label1');
  TextBox1 = $get('TextBox1');

  $addHandler(Label1, "click", Label1_Click);
  $addHandler(TextBox1, "blur", TextBox1_Blur);
  $addHandler(TextBox1, "keydown", TextBox1_KeyDown);
}

function Label1_Click() {
  TextBox1.value = Label1.innerHTML;
   
  Label1.style.display = 'none';
  TextBox1.style.display = '';
  
  TextBox1.focus();
}

 
function TextBox1_KeyDown(event) {
  if (event.keyCode == 13) {
    event.preventDefault();
    TextBox1.blur();
  }
}

function TextBox1_Blur() {
  var labelUpdated;
  
  if (Label1.textContent == TextBox1.value)
    labelUpdated = false;
  else 
    labelUpdated = true;
    
  Label1.innerHTML = TextBox1.value;
  
  TextBox1.style.display = 'none';
  Label1.style.display = '';
  
  if (labelUpdated)
    PageMethods.SetTitle(TextBox1.value);
}