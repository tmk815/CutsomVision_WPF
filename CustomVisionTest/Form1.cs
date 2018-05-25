using Microsoft.Cognitive.CustomVision.Prediction;
using Microsoft.Cognitive.CustomVision.Prediction.Models;
using System;
using System.Windows.Forms;

namespace CustomVisionTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 変数定義
            bool food = false; // "food" タグの有無
            string tag = ""; // カテゴリタグ

            ImageUrl url = new ImageUrl(textBox1.Text); //TextBoxからURLを取得

            var cvEp = new PredictionEndpoint {ApiKey = "PREDICTION_KEY" };
            var cvGuid =new Guid("PROJECT_ID");
            var iterationId = new Guid("iterationId");

            try
            {
                // 画像を判定
                var cvResult = cvEp.PredictImageUrl(cvGuid, url, iterationId);
                foreach (var item in cvResult.Predictions)
                {
                    if (item.Probability > 0.8)
                    {
                        if (item.Tag == "食べ物")
                        {
                            food = true;
                        }
                        else
                        {
                            tag = item.Tag;
                            break;
                        }
                    }
                }

                if (tag != "")
                {
                    // タグに応じてメッセージをセット
                    label1.Text = "この写真は " + tag + " です。";
                }
                else if (food == true)
                {
                    //msg = "I'm not sure what it is ...";
                    label1.Text = "これは食べ物ってことしか分からないです。";
                }
                else
                {
                    //msg = "Send me food photo you are eating!";
                    label1.Text = "食べ物の写真を送ってください。";
                }
            }
            catch(Exception)
            {
                label1.Text = "エラー";
            }
        }

    }
}
