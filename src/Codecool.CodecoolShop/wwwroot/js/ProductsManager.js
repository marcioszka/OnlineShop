import { dataHandler } from "./DataHandler.js";
import { domManager } from "./DomManager.js";
import { htmlFactory } from "./HtmlFactory.js";
import { cartManager } from "./CartManager.js";

export const productsManager = {
    loadProducts: async function () {
        const cShop = document.getElementById("cShop");
        const currentPath = cShop.dataset.path;
        if (currentPath === "/") {
            const products = await dataHandler.getProducts();
            generateProductCards(products);
        }
    },

    loadProductsByCategory: async function (categoryId) {
        const products = await dataHandler.getProductsByCategory(categoryId);
        generateProductCards(products);
    },

    loadProductsBySupplier: async function (supplierId) {
        const products = await dataHandler.getProductsBySupplier(supplierId);
        generateProductCards(products);
    }
};

function addToCart(clickEvent) {
    clickEvent.preventDefault();
    const target = clickEvent.target;
    cartManager.addProduct(target.dataset.product.split("-")[1]);
}

function generateProductCards(products) {
    domManager.removeChildren("#productList");
    for (let product of products) {
        const productCard = htmlFactory.cardBuilder(product);
        domManager.addChild("#productList", productCard);
        const element = document.getElementById("product" + product.Id);
        domManager.addEventListener(element, "click", addToCart);
    }
}
