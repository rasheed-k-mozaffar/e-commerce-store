// Create the navbar element
let navbar = document.createElement("header");

// Create the logo element
let logo = document.createElement("div");
logo.classList.add("logo");
logo.textContent = "Lab Store";
navbar.appendChild(logo);

// Create the hamburger element
let hamburger = document.createElement("div");
hamburger.classList.add("hamburger");
for (let i = 0; i < 3; i++) {
  let div = document.createElement("div");
  hamburger.appendChild(div);
}
navbar.appendChild(hamburger);

// Create the nav element
let nav = document.createElement("nav");
let ul = document.createElement("ul");

// Create the list items for the navigation links
let navItems = ["Home", "My Cart", "Contact", "About"];
let navLinks = ["home.html", "myCart.html", "contact.html", "about.html"];
for (let i = 0; i < navItems.length; i++) {
  let li = document.createElement("li");
  let a = document.createElement("a");
  a.href = navLinks[i];
  a.textContent = navItems[i];
  li.appendChild(a);
  ul.appendChild(li);
}
nav.appendChild(ul);
navbar.appendChild(nav);

// Add the navbar to the document body
document.body.appendChild(navbar);

// Add an event listener to the hamburger element
hamburger.addEventListener("click", function() {
  nav.classList.toggle("checked");
});
