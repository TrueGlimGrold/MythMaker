// ! Change hamburger icon
window.onload = function() {
    const primaryNav = document.querySelector(".primaryNav");
    const hamburgerBtn = document.querySelector(".hamburgerBtn");
    const mainContent = document.querySelector(".main-content");

    if (!primaryNav.classList.contains("open")) {
        hamburgerBtn.classList.add("closed");
    }
  };

function toggleMenu() {
    const primaryNav = document.querySelector(".primaryNav");
    const hamburgerBtn = document.querySelector(".hamburgerBtn");
    const mainContent = document.querySelector(".main-content");

    primaryNav.classList.toggle("open");
    hamburgerBtn.classList.toggle("open");
    mainContent.classList.toggle("open");


    if (!primaryNav.classList.contains("open")) {
        hamburgerBtn.classList.add("closed");
        
    } else {
        hamburgerBtn.classList.remove("closed");
    }
}

const x = document.querySelector(".hamburgerBtn");
x.onclick = toggleMenu;

// ! Open sign in window

// Get the modal
var modal = document.getElementById("userModal");

// Get the button that opens the modal
var btn = document.querySelector(".icon-nav");

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// Elements to toggle between sign in and sign up
var signInForm = document.getElementById("signInForm");
var signUpForm = document.getElementById("signUpForm");
var modalTitle = document.getElementById("modal-title");

var showSignUp = document.getElementById("showSignUp");
var showSignIn = document.getElementById("showSignIn");

// When the user clicks on the button, open the modal
btn.onclick = function() {
    modal.style.display = "block";
    signInForm.style.display = "block";
    signUpForm.style.display = "none";
    modalTitle.innerText = "Sign In";
}

// When the user clicks on <span> (x), close the modal
span.onclick = function() {
    modal.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function(event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}

// Switch to sign-up form
showSignUp.onclick = function() {
    signInForm.style.display = "none";
    signUpForm.style.display = "block";
    modalTitle.innerText = "Sign Up";
}

// Switch to sign-in form
showSignIn.onclick = function() {
    signInForm.style.display = "block";
    signUpForm.style.display = "none";
    modalTitle.innerText = "Sign In";
}

// ! Kart Dropdown

function toggleDropdown() {
    var dropdown = document.getElementById('dropdown-content');
    dropdown.style.display = dropdown.style.display === 'block' ? 'none' : 'block';
}

function viewCart() {
    // Redirect to cart page or open cart view
    window.location.href = '../veiws/cart.html';
}

function checkoutNow() {
    // Redirect to checkout page or open checkout view
    window.location.href = '../veiws/checkout.html';
}

// ryan