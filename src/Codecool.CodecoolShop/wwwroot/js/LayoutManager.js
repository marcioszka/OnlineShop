import { dataHandler } from "./DataHandler.js";
import { domManager } from "./DomManager.js";
import { htmlFactory } from "./HtmlFactory.js";
import { productsManager } from "./ProductsManager.js";

export const layoutManager = {
    loadLayoutElements: async function () {
        const cShop = document.getElementById("cShop");
        const currentPath = cShop.dataset.path;
        if (currentPath === "/") {
            const categories = await dataHandler.getCategories();
            const suppliers = await dataHandler.getSuppliers();

            for (let category of categories) {
                const categoryItem = htmlFactory.dropDownItemBuilder(category, "category");
                domManager.addChild("#dropdownMenuCategories", categoryItem);
                let element = document.getElementById("category" + category.Id);
                domManager.addEventListener(element, "click", loadProductsByCategory);
            }

            for (let supplier of suppliers) {
                const supplierItem = htmlFactory.dropDownItemBuilder(supplier, "supplier");
                domManager.addChild("#dropdownMenuSuppliers", supplierItem);
                let element = document.getElementById("supplier" + supplier.Id);
                domManager.addEventListener(element, "click", loadProductsBySupplier);
            }
        }
    }
};

function loadProductsBySupplier(clickEvent) {
    clickEvent.preventDefault();
    productsManager.loadProductsBySupplier(clickEvent.target.dataset.id);
}

function loadProductsByCategory(clickEvent) {
    clickEvent.preventDefault();
    productsManager.loadProductsByCategory(clickEvent.target.dataset.id);
}
