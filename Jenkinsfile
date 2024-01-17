pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build') {
            steps {
                script {
                    // Utilize o MSBuild para compilar o projeto .NET
                    bat 'dotnet build FiscalLabApp.sln'
                }
            }
        }

        stage('Publish') {
            steps {
                script {
                    // Publicar a aplicação
                    bat 'dotnet publish -c Release FiscalLabApp.sln'
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
