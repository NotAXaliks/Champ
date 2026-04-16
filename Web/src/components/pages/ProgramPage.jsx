import {
    Layout,
    Card,
    Typography,
    Row,
    Col,
    List,
    Tag,
    Button,
    Divider
} from "antd";
import { useState } from "react";

const { Title, Text } = Typography;

export function ProgramPage(data) {
    const stepInfo = {
        steps: [
            { name: "Загрузка материала", status: "done" },
            { name: "Смешивание", status: "done" },
            { name: "Выдержка", status: "done" },
            { name: "Экструзия", status: "active" },
            { name: "Охлаждение", status: "pending" },
            { name: "Контрольная точка", status: "pending" },
        ]
    }

    const [selectedStepIndex, setSelectedStepIndex] = useState(3);

    const selectedStep = data.steps[selectedStepIndex];

    const renderStatus = (status) => {
        switch (status) {
            case "done":
                return <Tag color="green">✓</Tag>;
            case "active":
                return <Tag color="blue">▶</Tag>;
            case "pending":
                return <Tag color="orange">⌛</Tag>;
            default:
                return null;
        }
    };

    return (
        <Layout>
            <Card>
                <Text><b>Партия:</b> B-101 <b>Продукт:</b> Гранулы A <b>Линия:</b> L-01 <b>Статус:</b> В работе</Text>
                <br />
                <Text><b>Текущий шаг:</b> {selectedStep.name} <b>Начало:</b> 12:01 <b>Отклонения:</b> —</Text>
            </Card>

            <div style={{ display: "flex" }}>
                <Card title="Шаги партии">
                    <List
                        dataSource={stepInfo.steps}
                        renderItem={(item, index) => {
                            const isSelected = index === selectedStepIndex;

                            return (
                                <List.Item
                                    onClick={() => setSelectedStepIndex(index)}
                                    style={{
                                        cursor: "pointer",
                                        background: isSelected ? "#e6f4ff" : "transparent",
                                    }}
                                >
                                    <Row style={{ width: "100%" }}>
                                        <Col span={2}>{index + 1}.</Col>
                                        <Col span={16}>{item.name}</Col>
                                        <Col span={2}>{renderStatus(item.status)}</Col>
                                    </Row>
                                </List.Item>
                            );
                        }}
                    />
                </Card>

                <Card title="Рабочая зона шага">
                    <Text><b>Шаг:</b> {selectedStep.name}</Text><br />
                    <Text><b>Тип:</b> {selectedStep.name}</Text><br />
                    <Text><b>Статус:</b> {selectedStep.status}</Text><br />
                    <Text>
                        <b>Инструкция:</b> Инструкция для шага "{selectedStep.name}"
                    </Text>

                    <Divider />

                    <Title level={5}>Данные шага / Параметры</Title>
                    <Text type="secondary">
                        Здесь отображаются данные для: {selectedStep.name}
                    </Text>

                    <Divider />

                    <Button type="primary">Начать шаг</Button>
                    <Button danger>Завершить</Button>
                </Card>
            </div>
        </Layout>
    );
}