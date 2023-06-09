export const dataHandler = {
    getProducts: async function () {
        return await apiGet("/get-products");
    },

    getCategories: async function () {
        return await apiGet("api/get-categories");
    },

    getSuppliers: async function () {
        return await apiGet("api/get-suppliers");
    },

    getProductsByCategory: async function (categoryId) {
        return await apiGet(`/get-products/category/${categoryId}`);
    },

    getProductsBySupplier: async function (supplierId) {
        return await apiGet(`/get-products/supplier/${supplierId}`);
    },

    getCartProducts: async function () {
        return await apiGet("api/get-cart-products");
    },

    addProductToCart: async function (productId) {
        return await apiPost(`cart/add/${productId}`);
    },

    increaseProductQuantity: async function (productId) {
        return await apiPut(`cart/increase/${productId}`);
    },

    removeProductFromCart: async function (productId) {
        return await apiPut(`cart/remove/${productId}`);
    },

    deleteProductFromCart: async function (productId) {
        return await apiDelete(`cart/delete/${productId}`);
    },

    clearCart: async function () {
        return await apiDelete(`cart/clear`);
    },
};

async function apiGet(url) {
    const response = await fetch(url);
    if (response.ok) {
        return response.json();
    } else {
        throw new Error(`GET request failed with status ${response.status}`);
    }
}

async function apiPost(url) {
    const response = await fetch(url, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
    });
    if (response.ok) {
        console.log("POST request succeeded");
    } else {
        throw new Error(`POST request failed with status ${response.status}`);
    }
}

async function apiDelete(url) {
    const response = await fetch(url, {
        method: "DELETE",
        headers: { "Content-Type": "application/json" },
    });
    if (response.ok) {
        console.log("DELETE request succeeded");
    } else {
        throw new Error(`DELETE request failed with status ${response.status}`);
    }
}

async function apiPut(url) {
    const response = await fetch(url, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
    });
    if (response.ok) {
        console.log("PUT request succeeded");
    } else {
        throw new Error(`PUT request failed with status ${response.status}`);
    }
}
