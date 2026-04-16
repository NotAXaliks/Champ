import { Avatar, Button, Divider, Form, Image, Input, Layout, Menu, Space, Tag, Typography } from "antd";
import { useState } from "react";
import { HttpClient } from "../Services";
import { BatchesPage } from "./pages/BatchesPage";
import { Header } from "antd/es/layout/layout";
import Sider from "antd/es/layout/Sider";
import { ProgramPage } from "./pages/ProgramPage";

const items = [
  {
    key: 'batches',
    label: 'Активные партии',
    page: <BatchesPage />
  },
  {
    key: 'program',
    label: 'Программа партии',
    page: <ProgramPage />
  },
  {
    key: 'live',
    label: 'Экструдер LIVE',
  },
  {
    key: 'journal',
    label: 'Журнал партии',
  },
  {
    key: 'report',
    label: 'Сообщить о проблеме',
  },
];

export default function ModulePage() {
    const [page, setPage] = useState("batches");

    const handleMenuClick = (values) => {
        setPage(values.key);
    }

    const currentPage = items.find((i) => i.key === page);

    return (
        <Layout style={{ minHeight: '100vh', minWidth: '100vw' }}>
            <Header style={{ display: "flex", justifyContent: "space-between" }}>
                <Space size="large">
                    <Space size="small">
                        <Image height={50} src="/assets/Logo.png" />
                        <Typography.Text strong style={{ color: '#fff', fontSize: 16 }}>
                            Система производства СЗР
                        </Typography.Text>
                    </Space>
                    
                    <Space>
                        <Typography.Text style={{ color: '#8c8c8c' }}>Раздел:</Typography.Text>
                        <Typography.Text strong style={{ color: '#fff' }}>
                            {currentPage.label}
                        </Typography.Text>
                    </Space>
                </Space>

                <Space size="large">
                    <Typography.Text style={{ color: '#fff' }}>Статус связи: Online</Typography.Text>
                   <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'flex-end' }}>
                        <Typography.Text style={{ color: '#fff', fontWeight: 500, lineHeight: 1.2 }}>
                            {HttpClient.user?.Name ?? ""}
                        </Typography.Text>
                        <Typography.Text style={{ color: '#8c8c8c', fontSize: 12, lineHeight: 1.2 }}>
                            {HttpClient.user?.Unit ?? "Юнит"}
                        </Typography.Text>
                    </div>
                </Space>
            </Header>

            <Layout>
                <Sider>
                    <Menu
                        mode="inline"
                        selectedKeys={[currentPage.key]}
                        items={items}
                        onClick={handleMenuClick}
                    />

                    <Space orientation="vertical">
                        <Typography.Text style={{ color: "white" }}>Смена: 2</Typography.Text>
                        <Typography.Text style={{ color: "white" }}>Линия: L-01</Typography.Text>
                    </Space>
                </Sider>

                <Layout>
                    <Layout.Content>
                        {currentPage.page}
                    </Layout.Content>
                </Layout>
            </Layout>
        </Layout>
    )
}
