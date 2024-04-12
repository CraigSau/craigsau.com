class Header extends HTMLElement {
  constructor() {
    super();
  }

  connectedCallback() {
    this.innerHTML = `
    <header>
         <nav class="nav">
             <ul class="nav-bar">
                 <li class="logo">
                     <h1><a href="/index.html">CraigSau</a></h1>
                 </li>
                 <input type="checkbox" id="check" />
                 <span class="menu">
                     <li><a>Blog</a></li>
                     <div class="get-user">
                         <li><a href="/views/register/register.html">Register</a></li>
                         <li><a href="/views/login/login.html">Log-In</a></li>
                     </div>
                     <label for="check" class="close-menu">
                         <i class="fa-solid fa-xmark" id="x-button"></i>
                     </label>
                 </span>
                 <label for="check" class="open-menu">
                     <i class="fa-solid fa-bars"></i>
                 </label>
             </ul>
         </nav>
    </header> 
    `;
  }
}

customElements.define('header-component', Header);
