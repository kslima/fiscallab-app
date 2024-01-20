pipeline {
    agent any

    environment {
            APP_FOLDER = 'app'
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build') {
            steps {
                script {
                    bat 'dotnet build FiscalLabApp.sln'
                }
            }
        }

        stage('Publish') {
            steps {
                script {
                    // Cria a pasta específica para a aplicação
                    bat "mkdir ${APP_FOLDER}"

                    bat "xcopy /s /y .\\bin\\Release\\* ${APP_FOLDER}"

                    dir("${APP_FOLDER}") {
                        bat 'dotnet publish -c Release FiscalLabApp.sln'
                    }
                }
            }
        }

        stage('Deploy') {
            steps {
                script {
                    // Inicie a aplicação após a implantação
                    dir("${APP_FOLDER}/bin/Release/net8.0/publish") {
                        bat 'dotnet FiscalLabApp.dll'
                    }
                }
            }
        }
    }

    post {
        success {
        // Ações a serem realizadas em caso de sucesso
        }
        failure {
        // Ações a serem realizadas em caso de falha
        }
    }
}
