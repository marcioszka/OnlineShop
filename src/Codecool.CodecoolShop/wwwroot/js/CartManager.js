import { dataHandler } from "./DataHandler.js";
import { domManager } from "./DomManager.js";
import { htmlFactory } from "./HtmlFactory.js";

export let cartManager = {
    loadCart: async function () {
        const cShop = document.getElementById("cShop");
        const currentPath = cShop.dataset.path;
        if (currentPath == "/") {
            domManager.removeChildren("#cart-container");
            const products = await dataHandler.getCartProducts();
            let productsSum = 0;
            for (let product of products) {
                productsSum +=
                    parseFloat(product.DefaultPrice).toFixed(2) *
                    product.CartProduct.Quantity;
                const productCard = htmlFactory.cartItemBuilder(product);
                domManager.addChild("#cart-container", productCard);
            }
            const sumCard = htmlFactory.cartTotalBuilder(productsSum);
            domManager.addChild("#cart-container", sumCard);
            this.addEventsToCartItems();
            this.displayCartCount(products);
        }
    },

    addProduct: async function (productId) {
        await dataHandler.addProductToCart(productId);
        this.loadCart();
    },

    increaseProduct: async function (productId) {
        await dataHandler.increaseProductQuantity(productId);
        this.loadCart();
    },

    deleteProduct: async function (productId) {
        await dataHandler.deleteProductFromCart(productId);
        this.loadCart();
    },

    removeProduct: async function (productId) {
        await dataHandler.removeProductFromCart(productId);
        this.loadCart();
    },

    clearCart: async function () {
        await dataHandler.clearCart();
        this.loadCart();
    },

    addClearCartEvent: function () {
        const cartClearBtn = document.getElementById("cart-clear");
        cartClearBtn.addEventListener("click", (event) => {
            event.preventDefault();
            this.clearCart();
        });
    },

    addEventsToCartItems: function () {
        const increaseBtns = document.querySelectorAll(".product-increase");
        const decreaseBtns = document.querySelectorAll(".product-remove");
        const deleteBtns = document.querySelectorAll(".product-delete");

        for (let iBtn of increaseBtns) {
            iBtn.addEventListener("click", (event) => {
                event.preventDefault();
                const currentEvent = event.target;
                this.increaseProduct(currentEvent.dataset.increase.split("-")[1]);
            });
        }

        for (let dBtn of decreaseBtns) {
            dBtn.addEventListener("click", (event) => {
                event.preventDefault();
                const currentEvent = event.target;
                this.removeProduct(currentEvent.dataset.remove.split("-")[1]);
            });
        }

        for (let deBtn of deleteBtns) {
            deBtn.addEventListener("click", (event) => {
                event.preventDefault();
                const currentEvent = event.target;
                this.deleteProduct(currentEvent.dataset.delete.split("-")[1]);
            });
        }
    },

    displayCartCount: function (products) {
        const cartCountDisplay = document.getElementById("cart-quantity");
        let count = 0;
        for (let product of products) {
            count += product.CartProduct.Quantity;
        }
        cartCountDisplay.innerHTML = count;
    },
};
