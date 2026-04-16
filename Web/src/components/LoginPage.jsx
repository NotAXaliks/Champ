import { Button, Form, Input, Layout, Typography } from "antd";
import { useState } from "react";
import { HttpClient } from "../Services";

export default function LoginPage({ onLogin }) {
    const [error, setError] = useState("");

    const onFinish = async (values) => {
        const loginResult = await HttpClient.request("POST", "api/Auth/login", { Login: values.login, Password: values.password });
        if (loginResult.Error) return setError(loginResult.Error);

        setError("");

        HttpClient.token = loginResult.Data.Token;
        HttpClient.user = loginResult.Data.User;

        onLogin();
    };

    return (
        <Layout>
            <Layout.Content>
                <Form onFinish={onFinish}>
                    <Form.Item label={null}>
                        <Typography.Title level={3}>{"СИСТЕМА УПРАВЛЕНИЯ ПРОИЗВОДСТВОМ СЭР"}</Typography.Title>
                        <Typography.Title level={3}>{"МОДУЛЬ АППАРАТЧИКА"}</Typography.Title>
                    </Form.Item>

                    <Form.Item label="Логин" name="login" rules={[{ required: true, message: "Введите логин" }]}>
                        <Input />
                    </Form.Item>

                    <Form.Item label="Пароль" name="password" rules={[{ required: true, message: "Введите пароль" }]}>
                        <Input.Password />
                    </Form.Item>

                    <Form.Item label={null}>
                        <Button type="primary" htmlType="submit">
                            Войти
                        </Button>
                    </Form.Item>

                    <Form.Item label="Сообщение" hidden={!error}>
                        <Typography.Text>{error}</Typography.Text>
                    </Form.Item>
                </Form>
            </Layout.Content>
        </Layout>
    )
}
