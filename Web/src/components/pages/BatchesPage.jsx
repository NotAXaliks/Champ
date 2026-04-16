import { Layout, Typography, Card, Form, Input, Select, Table, Row, Col } from "antd";

const { Title, Text } = Typography;

export function BatchesPage() {
    const columns = [
        {
            title: "№ партии",
            dataIndex: "batch",
            key: "batch",
        },
        {
            title: "Продукт",
            dataIndex: "product",
            key: "product",
        },
        {
            title: "Линия",
            dataIndex: "line",
            key: "line",
        },
        {
            title: "Текущий шаг",
            key: "step",
            render: (stepIndex, data) => {
                return <Text>{data.steps.find((step) => step.status === "active").name}</Text>
            },
        },
        {
            title: "Статус",
            dataIndex: "status",
            key: "status",
            render: (value) => ({
                "Критично": <Text style={{ background: "red", color: "white" }}>{value}</Text>,
                "Предупреждение": <Text style={{ background: "yellow", color: "white" }}>{value}</Text>,
                "Штатно": <Text style={{ background: "green", color: "white" }}>{value}</Text>,
            }[value]),
        },
    ];

    const data = [
        {
            key: "3",
            batch: "B-103",
            product: "Гранулы C",
            line: "L-02",
            steps: [
                { name: "Загрузка материала", status: "done" },
                { name: "Смешивание", status: "done" },
                { name: "Выдержка", status: "done" },
                { name: "Экструзия", status: "done" },
                { name: "Охлаждение", status: "done" },
                { name: "Контрольная точка", status: "active" },
            ],
            status: "Критично",
        },
    ];

    return (
        <Layout>
            <Card>
                <Title>ФИЛЬТРЫ</Title>
                <Row gutter={16}>
                    <Col span={8}>
                        <Form.Item label="Поиск">
                            <Input />
                        </Form.Item>
                    </Col>

                    <Col span={4}>
                        <Form.Item label="Линия">
                            <Select options={[{ label: "L-01" }, { label: "L-02" }]} />
                        </Form.Item>
                    </Col>

                    <Col span={4}>
                        <Form.Item label="Статус">
                            <Select options={[{ label: "В работе" }, { label: "Предупреждение" }, { label: "Критично" }]} />
                        </Form.Item>
                    </Col>

                    <Col span={4}>
                        <Form.Item label="Продукт">
                            <Select options={[{ label: "Гранулы A" }, { label: "Гранулы B" }, { label: "Гранулы C" }]} />
                        </Form.Item>
                    </Col>
                </Row>
            </Card>

            <Card>
                <Title>Активные партии</Title>

                <Table
                    columns={columns}
                    dataSource={data}
                />
            </Card>
        </Layout>
    );
}