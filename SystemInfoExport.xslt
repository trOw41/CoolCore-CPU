<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html" encoding="UTF-8" indent="yes"/>

	<xsl:template match="/SystemInformationExport">
		<html>
			<head>
				<title>Systeminformationen Export</title>
				<style>
					body {
					font-family: Arial, sans-serif;
					margin: 20px;
					background-color: #FF000000;
					color: #333;
					}
					.container {
					max-width: 900px;
					margin: 20px auto;
					background-color: #ffffff;
					padding: 30px;
					border-radius: 8px;
					box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
					}
					h1 {
					color: #FFFFA500;
					text-align: center;
					margin-bottom: 25px;
					font-size: 2.2em;
					}
					h2 {
					color: #0056b3;
					border-bottom: 2px solid #e0e0e0;
					padding-bottom: 10px;
					margin-top: 35px;
					font-size: 1.8em;
					}
					.info-grid {
					display: grid;
					grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
					gap: 20px;
					margin-top: 20px;
					}
					.info-card {
					background-color: #f9f9f9;
					border: 1px solid #e0e0e0;
					border-radius: 5px;
					padding: 15px;
					box-shadow: 0 2px 5px rgba(0, 0, 0, 0.05);
					}
					.info-card strong {
					color: #555;
					display: block;
					margin-bottom: 5px;
					font-size: 1.1em;
					}
					.info-card span {
					display: block;
					color: #666;
					}
					.timestamp {
					text-align: right;
					color: #888;
					font-size: 0.9em;
					margin-top: 30px;
					}
					.footer {
					text-align: center;
					margin-top: 40px;
					padding-top: 20px;
					border-top: 1px solid #e0e0e0;
					color: #888;
					font-size: 0.85em;
					}
				</style>
			</head>
			<body>
				<div class="container">
					<h1>Systeminformationen Export</h1>
					<p class="timestamp">
						Exportiert am: <xsl:value-of select="@ExportTimestamp"/>
					</p>

					<h2>Allgemeine Systemdetails</h2>
					<div class="info-grid">
						<xsl:apply-templates select="SystemInfoEntry/OSSystem"/>
						<xsl:apply-templates select="SystemInfoEntry/SystemType"/>
						<xsl:apply-templates select="SystemInfoEntry/ComputerName"/>
						<xsl:apply-templates select="SystemInfoEntry/UserName"/>
						<xsl:apply-templates select="SystemInfoEntry/DomainName"/>
						<xsl:apply-templates select="SystemInfoEntry/ProcessorCount"/>
					</div>

					<h2>Speicherinformationen</h2>
					<div class="info-grid">
						<xsl:apply-templates select="SystemInfoEntry/TotalPhysicalMemory"/>
						<xsl:apply-templates select="SystemInfoEntry/AvailablePhysicalMemory"/>
					</div>

					<h2>Netzwerkinformationen</h2>
					<div class="info-grid">
						<xsl:apply-templates select="SystemInfoEntry/HostName"/>
						<xsl:apply-templates select="SystemInfoEntry/IPAddresses"/>
						<xsl:apply-templates select="SystemInfoEntry/NetworkAdapterNames"/>
						<xsl:apply-templates select="SystemInfoEntry/NetworkAdapterMacAddresses"/>
					</div>

					<h2>Weitere Details</h2>
					<div class="info-grid">
						<xsl:apply-templates select="SystemInfoEntry/SystemDirectory"/>
						<xsl:apply-templates select="SystemInfoEntry/ProgramDirectory"/>
						<xsl:apply-templates select="SystemInfoEntry/BIOSVersion"/>
						<xsl:apply-templates select="SystemInfoEntry/ProcessorInformation"/>
						<xsl:apply-templates select="SystemInfoEntry/GraphicsCardInformation"/>
					</div>

					<div class="footer">
						Generiert von SystemINC - <xsl:value-of select="substring(@ExportTimestamp, 1, 4)"/>
					</div>
				</div>
			</body>
		</html>
	</xsl:template>

	<xsl:template match="OSSystem | SystemType | ComputerName | UserName | DomainName |
                     ProcessorCount | TotalPhysicalMemory | AvailablePhysicalMemory |
                     HostName | IPAddresses | SystemDirectory | ProgramDirectory |
                     NetworkAdapterNames | NetworkAdapterMacAddresses |
                     BIOSVersion | ProcessorInformation | GraphicsCardInformation">
		<div class="info-card">
			<strong>
				<xsl:choose>
					<xsl:when test="name() = 'OSSystem'">Betriebssystem</xsl:when>
					<xsl:when test="name() = 'SystemType'">System-Typ</xsl:when>
					<xsl:when test="name() = 'ComputerName'">PC/Host</xsl:when>
					<xsl:when test="name() = 'UserName'">Benutzer</xsl:when>
					<xsl:when test="name() = 'DomainName'">Domäne</xsl:when>
					<xsl:when test="name() = 'ProcessorCount'">Prozessorkerne</xsl:when>
					<xsl:when test="name() = 'TotalPhysicalMemory'">RAM (Phys. Gesamt)</xsl:when>
					<xsl:when test="name() = 'AvailablePhysicalMemory'">RAM (Phys. Frei)</xsl:when>
					<xsl:when test="name() = 'HostName'">Hostname</xsl:when>
					<xsl:when test="name() = 'IPAddresses'">IP-Adressen</xsl:when>
					<xsl:when test="name() = 'SystemDirectory'">Systemverzeichnis</xsl:when>
					<xsl:when test="name() = 'ProgramDirectory'">Programmverzeichnis</xsl:when>
					<xsl:when test="name() = 'NetworkAdapterNames'">Netzwerkadapter</xsl:when>
					<xsl:when test="name() = 'NetworkAdapterMacAddresses'">MAC-Adressen</xsl:when>
					<xsl:when test="name() = 'BIOSVersion'">BIOS-Version</xsl:when>
					<xsl:when test="name() = 'ProcessorInformation'">CPU-Information</xsl:when>
					<xsl:when test="name() = 'GraphicsCardInformation'">GPU-Information</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="name()"/>
					</xsl:otherwise>
				</xsl:choose>
			</strong>
			<span>
				<xsl:value-of select="."/>
			</span>
		</div>
	</xsl:template>

</xsl:stylesheet>