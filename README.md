# 🏢 Sistema de Gestão

Sistema completo de gestão de clientes, produtos e vendas desenvolvido em **Windows Forms** com **C#** e **SQLite**.

## 📋 Funcionalidades

- ✅ Cadastro de Clientes
- ✅ Cadastro de Produtos
- ✅ Registro de Vendas
- ✅ Consulta de Vendas
- ✅ Controle de Estoque Automático
- ✅ Interface Intuitiva

## 🛠️ Tecnologias Utilizadas

- **C# .NET Framework 4.8**
- **Windows Forms**
- **SQLite** (System.Data.SQLite 1.0.118)
- **ADO.NET**

## 📦 Estrutura do Projeto

SistemaGestao/ ├── Models/ # Modelos de dados (Cliente, Produto, Venda) ├── DAL/ # Camada de acesso a dados ├── Forms/ # Formulários da aplicação └── DatabaseHelper.cs # Gerenciamento do banco de dados

## 🚀 Como Executar

1. Clone o repositório:
<div class="widget code-container remove-before-copy"><div class="code-header non-draggable"><span class="iaf s13 w700 code-language-placeholder">bash</span><div class="code-copy-button"><span class="iaf s13 w500 code-copy-placeholder">Copiar</span><img class="code-copy-icon" src="data:image/svg+xml;utf8,%0A%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20width%3D%2216%22%20height%3D%2216%22%20viewBox%3D%220%200%2016%2016%22%20fill%3D%22none%22%3E%0A%20%20%3Cpath%20d%3D%22M10.8%208.63V11.57C10.8%2014.02%209.82%2015%207.37%2015H4.43C1.98%2015%201%2014.02%201%2011.57V8.63C1%206.18%201.98%205.2%204.43%205.2H7.37C9.82%205.2%2010.8%206.18%2010.8%208.63Z%22%20stroke%3D%22%23717C92%22%20stroke-width%3D%221.05%22%20stroke-linecap%3D%22round%22%20stroke-linejoin%3D%22round%22%2F%3E%0A%20%20%3Cpath%20d%3D%22M15%204.42999V7.36999C15%209.81999%2014.02%2010.8%2011.57%2010.8H10.8V8.62999C10.8%206.17999%209.81995%205.19999%207.36995%205.19999H5.19995V4.42999C5.19995%201.97999%206.17995%200.999992%208.62995%200.999992H11.57C14.02%200.999992%2015%201.97999%2015%204.42999Z%22%20stroke%3D%22%23717C92%22%20stroke-width%3D%221.05%22%20stroke-linecap%3D%22round%22%20stroke-linejoin%3D%22round%22%2F%3E%0A%3C%2Fsvg%3E%0A" /></div></div><pre id="code-0u4kdhfyy" style="color:#111b27;background:#e3eaf2;font-family:Consolas, Monaco, &quot;Andale Mono&quot;, &quot;Ubuntu Mono&quot;, monospace;text-align:left;white-space:pre;word-spacing:normal;word-break:normal;word-wrap:normal;line-height:1.5;-moz-tab-size:4;-o-tab-size:4;tab-size:4;-webkit-hyphens:none;-moz-hyphens:none;-ms-hyphens:none;hyphens:none;padding:8px;margin:8px;overflow:auto;width:calc(100% - 8px);border-radius:8px;box-shadow:0px 8px 18px 0px rgba(120, 120, 143, 0.10), 2px 2px 10px 0px rgba(255, 255, 255, 0.30) inset"><code class="language-bash" style="white-space:pre;color:#111b27;background:none;font-family:Consolas, Monaco, &quot;Andale Mono&quot;, &quot;Ubuntu Mono&quot;, monospace;text-align:left;word-spacing:normal;word-break:normal;word-wrap:normal;line-height:1.5;-moz-tab-size:4;-o-tab-size:4;tab-size:4;-webkit-hyphens:none;-moz-hyphens:none;-ms-hyphens:none;hyphens:none"><span class="token" style="color:#7c00aa">git</span><span> clone https://github.com/SEU-USUARIO/SistemaGestao.git
</span></code></pre></div>

2. Abra o projeto no **Visual Studio**

3. Restaure os pacotes NuGet:
   - Clique com botão direito na solução
   - "Restaurar Pacotes NuGet"

4. Configure a plataforma para **x64**

5. Execute o projeto (**F5**)

## 📊 Banco de Dados

O sistema utiliza **SQLite** com as seguintes tabelas:

- **Clientes:** Id, Nome, Telefone, Email, Endereco
- **Produtos:** Id, Nome, Preco, QuantidadeEstoque
- **Vendas:** Id, ClienteId, ProdutoId, Quantidade, ValorTotal, DataVenda

O banco de dados é criado automaticamente na primeira execução.

## 📸 Screenshots

### Tela Principal
<img width="978" height="685" alt="image" src="https://github.com/user-attachments/assets/d22f0301-f192-48db-878f-99fc9851270f" />

### Cadastro de Clientes
<img width="781" height="592" alt="image" src="https://github.com/user-attachments/assets/2e4032f5-d3e6-4e2e-8de9-8b42c758a26e" />

### Registro de Vendas
<img width="874" height="581" alt="image" src="https://github.com/user-attachments/assets/18c01e74-b97e-4c3c-9382-942758329804" />



