let instance = axios.create({
    baseURL: "http://api/",
    timeout: 1500000,
});

// 请求拦截器
instance.interceptors.request.use(
    function (config) {
        config.headers['authorization'] = 'Bearer ';

        return config;
    },
    function (error) {
        return Promise.reject(error);
    }
);

// 响应拦截器
instance.interceptors.response.use(
    function (response) {
        // 正常返回的异常,显示提示信息
        if (!response.data.Succeeded) {
            console.warn('服务器返回的异常:', response.data.Errors);
        }
        return response.data;
    },
    function (error) {
        // 对响应错误做点什么
        console.error(error);
        alert("发生了错误")
        return Promise.reject(error);
    }
);

export let myGet = function (url) {
    return instance.get(url);
}

export let myPost = function (url, data) {
    return instance.post(url, data);
}

export let myPut = function (url, data) {
    return instance.put(url, data);
}

export let myDel = function (url) {
    return instance.delete(url);
}
