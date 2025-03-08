
# ScreenCaptureApp

Este programa captura a tela em intervalos definidos, salva as imagens em um diretório específico e utiliza o Tesseract OCR para realizar o reconhecimento de texto nas imagens. Ele é útil para coletar dados e treinar o Tesseract com novos caracteres.

## Requisitos

- **.NET Framework 5.0** ou superior.
- **Tesseract v5.5.0.20241111**.
  
  O Tesseract OCR requer algumas bibliotecas específicas para funcionar corretamente. A versão utilizada neste programa é a **v5.5.0.20241111**, que depende das seguintes bibliotecas:
  
  ```
  Found AVX2
  Found AVX
  Found FMA
  Found SSE4.1
  Found libarchive 3.7.7 zlib/1.3.1 liblzma/5.6.3 bz2lib/1.0.8 liblz4/1.10.0 libzstd/1.5.6
  Found libcurl/8.11.0 Schannel zlib/1.3.1 brotli/1.1.0 zstd/1.5.6 libidn2/2.3.7 libpsl/0.21.5 libssh2/1.11.0
  ```

## Instalação do Tesseract

### Windows:

1. **Baixar e instalar o Tesseract**:
   - Acesse o link [Tesseract OCR - GitHub Releases](https://github.com/tesseract-ocr/tesseract/releases/tag/5.5.0).
   - Baixe o instalador mais recente para Windows (exemplo: `tesseract-5.5.0-win64.exe`).
   - Siga as instruções para instalar o Tesseract.

2. **Configuração do PATH**:
   - Durante a instalação, marque a opção para adicionar o Tesseract ao seu `PATH` do sistema (ou faça isso manualmente).
   - Se a opção não foi marcada durante a instalação, você pode adicionar manualmente o caminho de instalação do Tesseract:
   
     1. Abra o **Painel de Controle** e procure por "Variáveis de Ambiente".
     2. Clique em "Variáveis de Ambiente" e, em seguida, em "Novo" na seção **Variáveis de Sistema**.
     3. No campo **Nome da variável**, digite `TESSDATA_PREFIX` e no campo **Valor da variável**, coloque o caminho para o diretório do Tesseract (exemplo: `C:\Program Files\Tesseract-OCR\`).
     4. Encontre a variável `Path` em **Variáveis do Sistema** e clique em "Editar". Adicione o caminho para o diretório de instalação do Tesseract (exemplo: `C:\Program Files\Tesseract-OCR\`).

3. **Verifique a instalação**:
   Abra o prompt de comando (cmd) e execute:

   ```bash
   tesseract --version
   ```

   Isso deve retornar a versão do Tesseract, como `tesseract v5.5.0.20241111`.

### Linux (Ubuntu):

1. **Instalar o Tesseract**:
   Execute os seguintes comandos no terminal:

   ```bash
   sudo apt update
   sudo apt install tesseract-ocr
   ```

2. **Verificar a instalação**:
   Para garantir que o Tesseract foi instalado corretamente, execute:

   ```bash
   tesseract --version
   ```

### macOS:

1. **Instalar via Homebrew**:
   Execute os seguintes comandos no terminal:

   ```bash
   brew install tesseract
   ```

2. **Verificar a instalação**:
   Para garantir que o Tesseract foi instalado corretamente, execute:

   ```bash
   tesseract --version
   ```

3. **Instale as bibliotecas**
   ```bash
   dotnet add package Tesseract
   dotnet add package System.Drawing.Common
   ```
## Como Usar

1. Clone ou baixe o repositório do **ScreenCaptureApp**.
2. Abra o projeto no Visual Studio ou no seu editor de preferência.
3. Execute o programa e comece a capturar a tela.
   ```bash
   dotnet run
   ```
4. O programa irá salvar as imagens capturadas e usar o Tesseract OCR para realizar o reconhecimento de texto.

## Contribuindo

Sinta-se à vontade para fazer melhorias, corrigir bugs ou adicionar novos recursos. Envie um pull request ou crie um problema (issue) caso precise de ajuda.

## Licença

Este projeto é licenciado sob a [MIT License](LICENSE).
