# 当前进度：Task 1 已实现并验证

详见 TASK1.md。下方为原始准备大纲，TODO 状态以 TASK1.md 为准。

# Week 9 — PARCB 准备与大纲

按用户指定名称使用 PARCB。课程截图共有 3 个 Task，总计 120 分钟。

| Task | 课程时间 | 工作内容 | 交付内容 |
|---|---|---|---|
| 1：Dijkstra's Algorithm | 70 分钟 | 改造加权图、建立 Horse graph、实现最短路径和路径回溯 | 算法截图、Start 到 End 的控制台截图、两项反思 |
| 2：AI Code Generation | 25 分钟 | 使用 AI 生成可通过 dotnet run 运行的完整版本，与自己的实现对比 | Attachments 中的 AI 源码、六项反思 |
| 3：Wrapping up | 25 分钟 | 回顾学期学习、复习计划和课程建议 | 七项个人反思 |

## 已完成的准备
- 使用现有 PracA 的非泛型 Node / Graph 风格，改为 Dictionary<Node, int> 加权邻接表。
- 配置 net10.0，global.json 选择已安装的稳定版 SDK，避免默认选到预览版。
- 按截图重建 16 个节点、23 条无向边；与蜂节点相连的边权为 5，其余为 1。
- 初始化 DistanceList、PriorityQueue、parents；主程序仅做环境和图结构自检。
- 分离 AIComparison、Attachments、Screenshots，并准备反思模板。
- 配置 VS Code 构建、运行及 F5 调试。

## 运行
在 PARCB 目录打开终端：

```powershell
dotnet build
dotnet run
```

用 VS Code 打开 PARCB.code-workspace，可加载本项目专属任务与调试配置。
Ctrl+Shift+B 构建；F5 调试；“终端 > 运行任务 > PARCB: run”运行。

## 建议实施顺序
1. Task 1 前 15 分钟：理解 Node、Graph、HorseGraph，核对图结构；迁移 BFS/DFS 时遍历 Edges.Keys，或用键值对的 Key 获取邻居。
2. 接着 30 分钟：实现优先队列循环与松弛；只在距离变小时更新父节点并入队，跳过过期队列项。
3. 接着 15 分钟：处理无法到达的情况，回溯并反转路径，运行检查。
4. 最后 10 分钟：保存两张截图并写 Task 1 反思。
5. Task 2：5 分钟记录提示词；10 分钟生成并运行独立 AI 版本；10 分钟对比、保存源码并填写反思。
6. Task 3：25 分钟填写七项个人反思。

预期最优路径：Start -> A -> C -> F -> G -> H -> K -> N -> M -> L -> End；总权重 10。
这是课程给出的验收目标，当前骨架尚未实现或计算最短路径。

## 完成清单
- [ ] 自己实现 Dijkstra 循环及路径回溯。
- [ ] 迁移并检查原有 BFS / DFS。
- [ ] 验证最短路径与总权重，保存真实运行截图。
- [ ] 完成 Task 1 两项反思。
- [ ] 生成、运行并保留独立 AI 版本，记录实际提示词与修改次数。
- [ ] 完成 Task 2 六项比较反思。
- [ ] 完成 Task 3 七项个人反思。

说明：截图中的 Graph setup 代码处于折叠状态，因此 HorseGraph 按可见图重建。未修改原有 PracA。

