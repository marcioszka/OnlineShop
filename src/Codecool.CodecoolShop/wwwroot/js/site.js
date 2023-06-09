import { productsManager } from "./ProductsManager.js";
import { layoutManager } from "./LayoutManager.js";
import { cartManager } from "./CartManager.js";

const init = () => {
    layoutManager.loadLayoutElements();
    productsManager.loadProducts();

    const manageLink = document.querySelector('[href="/Identity/Account/Manage"]');
    if (manageLink !== null) {
        cartManager.loadCart();
        cartManager.addClearCartEvent();
    }
};

init();
