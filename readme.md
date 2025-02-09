# エクセルでローカルLLM
- [内容についてはこちらの記事](https://qiita.com/msms/items/3bf7e62753b2d656101a)
- NvidiaのGPUであれば[そのまま](https://github.com/msmsrep/ExcelAddinLLM/releases/tag/release)使えるはずです
- [Llama-3-ELYZA-JP-8B-GGUF](https://huggingface.co/elyza/Llama-3-ELYZA-JP-8B-GGUF/blob/main/Llama-3-ELYZA-JP-8B-q4_k_m.gguf)をダウンロードしてxllと同じ場所に保存しておく




# ビルド
- dotnet publish -c Release
