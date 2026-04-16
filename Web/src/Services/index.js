export class HttpClient {
    static token = "";
    static user = null;

    static async request(method, endpoint, body) {
        const options = { method, headers: { "Content-Type": "application/json" } };
        if (body) options.body = JSON.stringify(body);
        if (this.token) options.headers.Authorization = `Bearer ${this.token}`;

        try {
            const data = await fetch(`http://localhost:5261/${endpoint}`, options);
            return data.json()
        } catch {
            return { Error: "Ошибка запроса" }
        }
    }
}
